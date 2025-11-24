using System;
using System.ComponentModel;
using MeritoQuiz.Models;
using NUnit.Framework;

namespace MeritoQuiz.Tests.ViewModels;

public class AnswerTests
{
    [Test]
    public void Defaults_AreExpected()
    {
        var vm = new Answer();
        Assert.That(vm.Text, Is.EqualTo(string.Empty));
        Assert.That(vm.IsSelected, Is.False);
    }

    [Test]
    public void Setting_Text_Raises_PropertyChanged_Once()
    {
        var vm = new Answer();
        var changes = 0;
        string? lastName = null;
        vm.PropertyChanged += (_, e) => { if (e.PropertyName == nameof(Answer.Text)) { changes++; lastName = e.PropertyName; } };

        vm.Text = "Hello";
        vm.Text = "Hello"; // no change, should not raise

        Assert.That(changes, Is.EqualTo(1));
        Assert.That(lastName, Is.EqualTo(nameof(Answer.Text)));
    }

    [Test]
    public void Setting_IsSelected_Raises_PropertyChanged_Once()
    {
        var vm = new Answer();
        var changes = 0;
        vm.PropertyChanged += (_, e) => { if (e.PropertyName == nameof(Answer.IsSelected)) changes++; };

        vm.IsSelected = true;
        vm.IsSelected = true; // no change

        Assert.That(changes, Is.EqualTo(1));
        Assert.That(vm.IsSelected, Is.True);
    }
}
