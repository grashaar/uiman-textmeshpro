using UnityEngine;
using TMPro;

namespace UnuGames.MVVM
{
    [RequireComponent(typeof(TMP_Text))]
    [DisallowMultipleComponent]
    public class TMP_NumberBinder : BinderBase
    {
        protected TMP_Text text;

        [HideInInspector]
        public BindingField valueField = new BindingField("Number");

        [HideInInspector]
        public FloatConverter valueConverter = new FloatConverter("Number");

        public string format;
        public float timeChange = 0.25f;

        public override void Initialize(bool forceInit)
        {
            if (!CheckInitialize(forceInit))
                return;

            this.text = GetComponent<TMP_Text>();
            SubscribeOnChangedEvent(this.valueField, OnUpdateText);
        }

        public void OnUpdateText(object newVal)
        {
            var oldValue = this.valueConverter.Convert(this.text.text, this);
            var newValue = this.valueConverter.Convert(newVal, this);

            UITweener.Value(this.gameObject, this.timeChange, oldValue, newValue)
                     .SetOnUpdate(SetValue)
                     .SetOnComplete(() => SetValue(newValue));
        }

        private void SetValue(float value)
        {
            if (string.IsNullOrEmpty(this.format))
            {
                this.text.text = value.ToString();
            }
            else
            {
                this.text.text = string.Format(this.format, value);
            }
        }
    }
}