using System;
using System.Collections.Generic;
using NbrbAPI.Models;

public interface IRateService
{
    IEnumerable<Rate> GetRates(DateTime date);
}