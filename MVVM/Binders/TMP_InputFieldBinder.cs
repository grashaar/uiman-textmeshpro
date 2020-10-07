﻿using UnityEngine;
using TMPro;

namespace UnuGames.MVVM
{
    [RequireComponent(typeof(TMP_InputField))]
    [DisallowMultipleComponent]
    public class TMP_InputFieldBinder : BinderBase
    {
        protected TMP_InputField input;

        [HideInInspector]
        public BindingField valueField = new BindingField("Text");

        [HideInInspector]
        public TwoWayBindingString onValueChanged = new TwoWayBindingString("On Value Changed");

        [HideInInspector]
        public StringConverter valueConverter = new StringConverter("Text");

        public override void Initialize(bool forceInit)
        {
            if (!CheckInitialize(forceInit))
                return;

            this.input = GetComponent<TMP_InputField>();

            SubscribeOnChangedEvent(this.valueField, OnUpdateValue);

            OnValueChanged_OnChanged(this.onValueChanged);
            this.onValueChanged.onChanged += OnValueChanged_OnChanged;
        }

        private void OnUpdateValue(object val)
        {
            var value = this.valueConverter.Convert(val, this);
            this.input.SetTextWithoutNotify(value);
        }

        private void OnValueChanged(string value)
        {
            SetValue(this.valueField, this.onValueChanged.converter.Convert(value, this));
        }

        private void OnValueChanged_OnChanged(bool value)
        {
            this.input.onValueChanged.RemoveListener(OnValueChanged);

            if (value)
                this.input.onValueChanged.AddListener(OnValueChanged);
        }
    }
}