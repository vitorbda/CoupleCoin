﻿using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;

namespace CoupleCoinApi.Repositories.Interfaces
{
    public interface IExpenseRepository
    {
        bool CreateExpenseType(ExpenseType ET);
        Expense CreateExpense(Expense expense);
        bool CreateExpenseXOwner(ExpenseXOwner expenseXOwner);
        ExpenseType GetActiveExpenseTypeById(int id);
    }
}
