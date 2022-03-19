using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectRecipe.Behaviors
{
    public class EventToCommandBehavior : BehaviorBase<VisualElement>
    {
        Delegate eventHandler;

        public static readonly BindableProperty EventNameProperty = BindableProperty.Create("EventName", typeof(string), typeof(EventToCommandBehavior), null, propertyChanged: OnEventNameChanged);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(EventToCommandBehavior), null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(EventToCommandBehavior), null);
        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create("Converter", typeof(IValueConverter), typeof(EventToCommandBehavior), null);

        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(InputConverterProperty); }
            set { SetValue(InputConverterProperty, value); }
        }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            RegisterEvent(EventName);
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            DeregisterEvent(EventName);
            base.OnDetachingFrom(bindable);
        }

        private void RegisterEvent(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName)) return;

            EventInfo eventInfo = AssociateObject.GetType().GetRuntimeEvent(eventName);
            if (eventInfo == null)
                throw new ArgumentException($"EventToCommandBehavior: Can't register the '{EventName}' event");

            MethodInfo methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");
            eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(AssociateObject, eventHandler);
        }

        private void DeregisterEvent(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName) || eventHandler == null) return;

            EventInfo eventInfo = AssociateObject.GetType().GetRuntimeEvent(eventName);
            if (eventName == null)
                throw new ArgumentException($"EventToCommandBehavior: Can't de-register the '{EventName}' event");

            eventInfo.RemoveEventHandler(AssociateObject, eventHandler);
            eventHandler = null;
        }

        void OnEvent(object sender, object eventArgs)
        {
            if (Command == null) return;

            object resolveParameter;
            if (CommandParameter != null)
                resolveParameter = CommandParameter;
            else if (Converter != null)
                resolveParameter = Converter.Convert(eventArgs, typeof(object), null, null);
            else
                resolveParameter = eventArgs;

            if (Command.CanExecute(resolveParameter))
                Command.Execute(resolveParameter);
        }

        private static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (EventToCommandBehavior)bindable;
            if (behavior.AssociateObject == null) return;

            string oldEventName = (string)oldValue;
            string newEventName = (string)newValue;

            behavior.DeregisterEvent(oldEventName);
            behavior.RegisterEvent(newEventName);
        }
    }
}
