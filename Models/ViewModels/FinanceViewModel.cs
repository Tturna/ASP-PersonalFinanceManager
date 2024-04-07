﻿using PersonalFinances.Models.DataTransferObjects;

namespace PersonalFinances.Models.ViewModels;

public class FinanceViewModel
{
    public class TransactionGroup
    {
        public required string MonthYear { get; init; }
        public bool IsCurrentMonth { get; init; }
        public List<TransactionModel> Transactions { get; init; } = [];
        public decimal TotalIncome { get; init; }
        public decimal TotalExpenses { get; init; }
        public decimal Total { get; init; }
    }
    
    // public List<TransactionModel> IncomeTransactions { get; init; }
    // public List<TransactionModel> ExpenseTransactions { get; init; }
    // List<TransactionModel> ConfirmedTransactions { get; init; }
    public List<TransactionGroup> SortedTransactionGroups { get; init; }
    public List<TransactionModel> ExpectedTransactionsThisMonth { get; init; }
    public TransactionGroup ExpectedTransactionGroupNextMonth { get; init; }

    public FinanceViewModel(List<TransactionModel> confirmedTransactions)
    {
        // ConfirmedTransactions = confirmedTransactions;
        // IncomeTransactions = ConfirmedTransactions.Where(t => t.IsIncome).ToList();
        // ExpenseTransactions = ConfirmedTransactions.Where(t => !t.IsIncome).ToList();
        
        var today = DateOnly.FromDateTime(DateTime.Now);
        var currentMonthYear = today.ToString("MMMM yyyy");
        var daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
        var daysLeftInMonth = daysInMonth - today.Day;

        // group transactions by year and month
        // TODO: Fix bug where future transactions of this month are shown treated as past transactions
        SortedTransactionGroups = confirmedTransactions
            .OrderByDescending(t => t.Date)
            .GroupBy(t => t.Date.ToString("MMMM yyyy"))
            .Select(g => new TransactionGroup
            {
                MonthYear = g.Key,
                IsCurrentMonth = g.Key == currentMonthYear,
                Transactions = g.ToList(),
                TotalIncome = g.Where(t => t.IsIncome).Sum(t => t.AmountEuro),
                TotalExpenses = g.Where(t => !t.IsIncome).Sum(t => t.AmountEuro),
                Total = g.Sum(t => t.AmountEuro)
            })
            .ToList();
        
        // fill in the gaps between the first transaction and today
        var oldestGroup = SortedTransactionGroups.LastOrDefault();
        if (oldestGroup != null)
        {
            var oldestGroupDate = DateOnly.Parse(oldestGroup.MonthYear);
            var iterationGroupDate = oldestGroupDate;
            var groupCount = SortedTransactionGroups.Count;

            // fill in between the first and last transaction group
            for (var i = groupCount - 2; i >= 0; i--)
            {
                var currentGroup = SortedTransactionGroups[i];
                var expectedGroupDate = iterationGroupDate.AddMonths(1);
                var expectedMonthYear = expectedGroupDate.ToString("MMMM yyyy");
                iterationGroupDate = expectedGroupDate;

                if (currentGroup.MonthYear == expectedMonthYear) continue;

                SortedTransactionGroups.Insert(i + 1, new TransactionGroup
                {
                    MonthYear = expectedMonthYear
                });

                i++;
            }
            
            // fill in between the newest transaction group and today
            while (iterationGroupDate.Month != today.Month || iterationGroupDate.Year != today.Year)
            {
                iterationGroupDate = iterationGroupDate.AddMonths(1);
                var fillerMonthYear = iterationGroupDate.ToString("MMMM yyyy");
                
                SortedTransactionGroups.Insert(0, new TransactionGroup
                {
                    MonthYear = fillerMonthYear,
                    IsCurrentMonth = fillerMonthYear == currentMonthYear
                });
            }
        }

        ExpectedTransactionsThisMonth = [];
        ExpectedTransactionGroupNextMonth = new TransactionGroup
        {
            MonthYear = today.AddMonths(1).ToString("MMMM yyyy")
        };
        
        var nextMonth = today.AddMonths(1);
        var daysInNextMonth = DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month);

        foreach (var transaction in confirmedTransactions)
        {
            var trDate = transaction.Date;
            if (transaction.Reoccurrence == Reoccurrence.Daily)
            {
                for (var i = 0; i < daysLeftInMonth; i++)
                {
                    ExpectedTransactionsThisMonth.Add(transaction.Clone(today.AddDays(i + 1)));
                }

                for (var i = 0; i < daysInNextMonth; i++)
                {
                    var newDate = new DateOnly(today.Year, today.Month, 1).AddMonths(1).AddDays(i);
                    ExpectedTransactionGroupNextMonth.Transactions.Add(transaction.Clone(newDate));
                }
            }
            else if (transaction.Reoccurrence == Reoccurrence.Weekly)
            {
                for (var i = 1; i < daysLeftInMonth; i++)
                {
                    if ((today.AddDays(i).DayNumber - trDate.DayNumber) % 7 == 0)
                    {
                        ExpectedTransactionsThisMonth.Add(transaction.Clone(today.AddDays(i)));
                    }
                }

                for (var i = 0; i < daysInNextMonth; i++)
                {
                    var newDate = new DateOnly(today.Year, today.Month, 1).AddMonths(1).AddDays(i);
                    if ((newDate.DayNumber - trDate.DayNumber) % 7 != 0) continue;
                    ExpectedTransactionGroupNextMonth.Transactions.Add(transaction.Clone(newDate));
                    i += 6; // idk if this actually speeds up anything but I like to think it does
                }
            }
            else if (transaction.Reoccurrence == Reoccurrence.Monthly)
            {
                var expectedTrDay = trDate.Day > daysInNextMonth ? daysInNextMonth : trDate.Day;
                var expectedTrDate = new DateOnly(today.Year, today.Month, expectedTrDay).AddMonths(1);
                ExpectedTransactionGroupNextMonth.Transactions.Add(transaction.Clone(expectedTrDate));
                
                if (trDate.Day <= today.Day) continue;
                
                var newTrDay = trDate.Day > daysInMonth ? daysInMonth : trDate.Day;
                var newTrDate = new DateOnly(today.Year, today.Month, newTrDay);

                ExpectedTransactionsThisMonth.Add(transaction.Clone(newTrDate));
            }
            else if (transaction.Reoccurrence == Reoccurrence.Annually)
            {
                if (trDate.Month == today.Month)
                {
                    var newTrDate = new DateOnly(today.Year, trDate.Month, trDate.Day);
                    ExpectedTransactionsThisMonth.Add(transaction.Clone(newTrDate));
                }
                else if (trDate.Month == nextMonth.Month)
                {
                    var newTrDate = new DateOnly(today.Year, trDate.Month, trDate.Day).AddMonths(1);
                    ExpectedTransactionGroupNextMonth.Transactions.Add(transaction.Clone(newTrDate));
                }
            }
        }
        
        ExpectedTransactionsThisMonth = ExpectedTransactionsThisMonth.OrderByDescending(t => t.Date).ToList();
    }
}