namespace LR9_12.Views.Pages;

using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;

public partial class CalculatorView : UserControl
{
    private static readonly Dictionary<string, Func<double, double, double>> binaryOperationMapper = new()
    {
        {"+", (x, y) => x + y},
        {"-", (x, y) => x - y},
        {"*", (x, y) => x * y},
        {"/", (x, y) => {
            if (y == 0)
            {
                throw new DivideByZeroException();
            }
            return x / y;
        }}
    };
    private static readonly Dictionary<string, Func<double, double>> unaryOperationMapper = new()
    {
        {"=", x => x},
        {"+/-", x => -x},
        {"1/x", x => 1 / x},
        {"x^2", x => x * x},
        {"sqrt(x)", x => Math.Sqrt(x)},
        {"log(x)", x => Math.Log(x)}
    };

    private double result = 0;
    private double memory = 0;
    private string inputBuffer = "0";
    private string currentOperation = "";
    private bool resMod = false;
    private bool unaryLast = false;

    public CalculatorView()
    {
        InitializeComponent();
    }

    private void Clear()
    {
        expression.Text = "";
        inputBuffer = "0";
        result = 0;
        currentOperation = "";
        Update();
    }

    public void ClearHandler(object sender, RoutedEventArgs args)
    {
        Clear();
    }

    public void ClearEntryHandler(object sender, RoutedEventArgs args)
    {
        inputBuffer = "0";
        Update();
    }

    public void DeleteHandler(object sender, RoutedEventArgs args)
    {
        if (inputBuffer.Length > 0)
        {
            inputBuffer = inputBuffer[..^1];
            if (inputBuffer.Length == 0)
            {
                inputBuffer = "0";
            }
            Update();
        }
    }

    public void NumericHandler(object sender, RoutedEventArgs args)
    {
        if (sender is Button button)
        {
            if (button.Content is string num)
            {
                if (resMod)
                {
                    inputBuffer = "";
                    resMod = false;
                }
                inputBuffer += num;
                if (inputBuffer.Length > 1)
                {
                    inputBuffer = inputBuffer.TrimStart('0');
                }
                Update();
            }
        }
    }

    public void OperationHandler(object sender, RoutedEventArgs args)
    {
        if (sender is Button button)
        {
            if (button.Content is string op && button.Tag is string tag)
            {
                try
                {
                    resMod = true;

                    if (currentOperation == "")
                    {
                        result = double.Parse(inputBuffer);
                    }
                    if (binaryOperationMapper.ContainsKey(currentOperation))
                    {
                        result = binaryOperationMapper[currentOperation](result, double.Parse(inputBuffer));
                    }
                    if (binaryOperationMapper.ContainsKey(tag))
                    {
                        expression.Text += $"{(!unaryLast ? inputBuffer + " " : "")}{op} ";
                        currentOperation = tag;
                        unaryLast = false;
                    }
                    else if (unaryOperationMapper.ContainsKey(tag))
                    {
                        result = unaryOperationMapper[tag](result);
                        if (tag != "+/-")
                        {
                            expression.Text += $"{op.Replace("x", inputBuffer)} ";
                            unaryLast = true;
                        }
                    }
                    inputBuffer = result.ToString();
                    Update();
                }
                catch (DivideByZeroException)
                {
                    Clear();
                    numberField.Text = "Cannot divide by zero";
                }
                catch (Exception)
                {
                    Clear();
                    numberField.Text = "ERROR";
                }
            }
        }
    }

    public void ResultHandler(object sender, RoutedEventArgs args)
    {
        try
        {
            resMod = true;

            if (binaryOperationMapper.ContainsKey(currentOperation))
            {
                result = binaryOperationMapper[currentOperation](result, double.Parse(inputBuffer));
            }
            currentOperation = "";
            expression.Text = "";
            inputBuffer = result.ToString();
            result = 0;
            Update();
        }
        catch (DivideByZeroException)
        {
            Clear();
            numberField.Text = "Cannot divide by zero";
        }
        catch (Exception)
        {
            Clear();
            numberField.Text = "ERROR";
        }
    }

    public void MemoryHandler(object sender, RoutedEventArgs args)
    {
        if (sender is Button button)
        {
            if (button.Content is string op)
            {
                switch (op)
                {
                    case "MC":
                        memory = 0;
                        break;
                    case "MR":
                        inputBuffer = memory.ToString();
                        Update();
                        break;
                    case "M+":
                        memory += inputBuffer.Length > 0 ? double.Parse(inputBuffer) : 0;
                        break;
                    case "M-":
                        memory -= double.Parse(inputBuffer);
                        break;
                    case "MS":
                        memory = double.Parse(inputBuffer);
                        break;
                }
            }
        }
    }

    public void Update()
    {
        numberField.Text = inputBuffer;
    }
}