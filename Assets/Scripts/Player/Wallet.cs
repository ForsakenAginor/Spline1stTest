using System;
using System.Linq;

public class Wallet
{
    private int _currency;

    public Wallet(int currency)
    {
        _currency = currency >= 0 ? currency : throw new ArgumentOutOfRangeException(nameof(currency));
    }

    public event Action Broke;
    public event Action CurrencyChanged;

    public int Currency => _currency;

    public void AddCurrency(int amount)
    {
        if(amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        _currency += amount;
        CurrencyChanged?.Invoke();
    }

    public void SpentCurrency(int amount)
    {
        if(amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        _currency -= amount;
        CurrencyChanged?.Invoke();

        if (_currency < 0)
            Broke?.Invoke();
    }
}
