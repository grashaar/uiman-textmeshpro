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

        public string format;
        public float timeChange = 0.25f;

        public override void Initialize(bool forceInit)
        {
            if (!CheckInitialize(forceInit))
                return;

            this.text = GetComponent<TMP_Text>();
            SubscribeOnChangedEvent(this.valueField, OnUpdateText);
        }

        public void OnUpdateText(object newNumber)
        {
            if (newNumber == null)
                return;

            double.TryParse(this.text.text, out var val);
            double.TryParse(newNumber.ToString(), out var change);

            UITweener.Value(this.gameObject, this.timeChange, (float)val, (float)change)
                     .SetOnUpdate(OnUpdate)
                     .SetOnComplete(() => OnComplete(newNumber));
        }

        private void OnUpdate(float val)
        {
            if (string.IsNullOrEmpty(this.format))
            {
                this.text.text = val.ToString();
            }
            else
            {
                this.text.text = string.Format(this.format, val);
            }
        }

        private void OnComplete(object newNumber)
        {
            var text = newNumber.ToString();

            if (string.IsNullOrEmpty(this.format))
            {
                this.text.text = text;
            }
            else
            {
                this.text.text = string.Format(this.format, text);
            }
        }
    }
}