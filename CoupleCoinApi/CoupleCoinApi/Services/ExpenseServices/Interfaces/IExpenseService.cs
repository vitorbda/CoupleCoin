﻿using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;

namespace CoupleCoinApi.Services.ExpenseServices.Interfaces
{
    public interface IExpenseService
    {
        Task<ValidateRegisterModel> VerifyCouple(ExpenseTypeDTO ETD);
        bool RegisterExpenseType(ExpenseTypeDTO ETD);
    }
}
