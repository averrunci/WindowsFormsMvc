// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Carna;
using Charites.Windows.Mvc;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Forms
{
    [Specification("ObservablePropertyBindings Spec")]
    class ObservablePropertyBindingsSpec : FixtureSteppable
    {
        ObservablePropertyBindings ObservablePropertyBindings { get; } = new ObservablePropertyBindings();

        const string InitialName = "Initial";
        const string ChangedName = "Changed";
        ObservableProperty<string> NameProperty { get; } = InitialName.ToObservableProperty();

        const int InitialText = 7;
        const int ChangedText = 99;
        ObservableProperty<int> TextProperty { get; } = InitialText.ToObservableProperty();

        const string ControlInitialName = "Control Name";
        const string ControlInitialText = "0";
        TestControls.TestControl TestControl { get; } = new TestControls.TestControl
        {
            Name = ControlInitialName,
            Text = ControlInitialText
        };

        void SetControlPropertyValue(object value, Control control, string propertyName)
            => TypeDescriptor.GetProperties(control).OfType<PropertyDescriptor>().FirstOrDefault(p => p.Name == propertyName)?.SetValue(control, value);

        [Example("Binds the observable property to the property of the control")]
        void Ex01()
        {
            When("to bind the observable property to the name property of the control", () => ObservablePropertyBindings.Bind(NameProperty, TestControl, nameof(TestControl.Name)));
            Then("the value of the name property of the control should be the value of the observable property", () => TestControl.Name == NameProperty.Value);
            When("the value of the observable property is changed", () => NameProperty.Value = ChangedName);
            Then("the value of the name property of the control should be the changed value", () => TestControl.Name == ChangedName);

            When("to bind the observable property to the text property of the control with the converter", () => ObservablePropertyBindings.Bind(TextProperty, TestControl, nameof(TestControl.Text), text => text.ToString()));
            Then("the value of the text property of the control should be the value of the observable property", () => TestControl.Text == TextProperty.Value.ToString());
            When("the value of the observable property is changed", () => TextProperty.Value = ChangedText);
            Then("the value of the text property of the control should be the changed value.", () => TestControl.Text == ChangedText.ToString());

            When("to unbind bindings", () => ObservablePropertyBindings.Unbind());

            When("the value of the observable property for the name is changed", () => NameProperty.Value = InitialName);
            Then("the value of the name property of the control should not be changed", () => TestControl.Name == ChangedName);

            When("the value of the observable property for the text is changed", () => TextProperty.Value = InitialText);
            Then("the value of the text property of the control should not be changed", () => TestControl.Text == ChangedText.ToString());
        }

        [Example("Binds the observable property to the property of the control in two way direction")]
        void Ex02()
        {
            When("to bind the observable property to the name property of the control", () => ObservablePropertyBindings.BindTwoWay(NameProperty, TestControl, nameof(TestControl.Name)));
            Then("the value of the name property of the control should be the value of the observable property", () => TestControl.Name == NameProperty.Value);
            When("the value of the observable property is changed", () => NameProperty.Value = ChangedName);
            Then("the value of the name property of the control should be the changed value", () => TestControl.Name == ChangedName);
            When("the value of the name property of the control is changed", () => SetControlPropertyValue(InitialName, TestControl, nameof(TestControl.Name)));
            Then("the value of the observable property should be the changed value", () => NameProperty.Value == InitialName);

            When("to bind the observable property to the text property of the control with the converter and the back converter", () => ObservablePropertyBindings.BindTwoWay(TextProperty, TestControl, nameof(TestControl.Text), text => text.ToString(), int.Parse));
            Then("the value of the text property of the control should be the value of the observable property", () => TestControl.Text == TextProperty.Value.ToString());
            When("the value of the observable property is changed", () => TextProperty.Value = ChangedText);
            Then("the value of the text property of the control should be the changed value.", () => TestControl.Text == ChangedText.ToString());
            When("the value of the text property of the control is changed", () => SetControlPropertyValue(InitialText.ToString(), TestControl, nameof(TestControl.Text)));
            Then("the value of the observable property should be the changed value", () => TextProperty.Value == InitialText);

            When("to unbind bindings", () => ObservablePropertyBindings.Unbind());

            When("the value of the observable property for the name is changed", () => NameProperty.Value = ChangedName);
            Then("the value of the name property of the control should not be changed", () => TestControl.Name == InitialName);
            When("the value of the name property of the control is changed", () => SetControlPropertyValue("???", TestControl, nameof(TestControl.Name)));
            Then("the value of the observable property for the name should not be changed", () => NameProperty.Value == ChangedName);

            When("the value of the observable property for the text is changed", () => TextProperty.Value = ChangedText);
            Then("the value of the text property of the control should not be changed", () => TestControl.Text == InitialText.ToString());
            When("the value of the text property of the control is changed", () => SetControlPropertyValue("???", TestControl, nameof(TestControl.Text)));
            Then("the value of the observable property for the text should not be changed", () => TextProperty.Value == ChangedText);
        }

        [Example("Binds the observable property to the property of the control in the source direction")]
        void Ex03()
        {
            When("to bind the observable property to the name property of the control", () => ObservablePropertyBindings.BindToSource(NameProperty, TestControl, nameof(TestControl.Name)));
            Then("the value of the observable property should be the value of the name property of the control", () => NameProperty.Value == ControlInitialName);
            When("the value of the name property of the control is changed", () => SetControlPropertyValue(ChangedName, TestControl, nameof(TestControl.Name)));
            Then("the value of the observable property should be the changed value", () => NameProperty.Value == ChangedName);

            When("to bind the observable property to the text property of the control with the back converter", () => ObservablePropertyBindings.BindToSource(TextProperty, TestControl, nameof(TestControl.Text), (string text) => int.Parse(text)));
            Then("the value of the text property of the control should be the value of the observable property", () => TextProperty.Value == int.Parse(ControlInitialText));
            When("the value of the text property of the control is changed", () => SetControlPropertyValue(ChangedText.ToString(), TestControl, nameof(TestControl.Text)));
            Then("the value of the observable property should be the changed value", () => TextProperty.Value == ChangedText);

            When("to unbind bindings", () => ObservablePropertyBindings.Unbind());

            When("the value of the name property of the control is changed", () => SetControlPropertyValue(InitialName, TestControl, nameof(TestControl.Name)));
            Then("the value of the observable property for the name should not be changed", () => NameProperty.Value == ChangedName);

            When("the value of the text property of the control is changed", () => SetControlPropertyValue(InitialText.ToString(), TestControl, nameof(TestControl.Text)));
            Then("the value of the observable property for the text should not be changed", () => TextProperty.Value == ChangedText);
        }
    }
}
