using System;
using TMPro;

namespace UnuGames.MVVM
{
    [Serializable]
    public sealed class TMP_DropdownOptionConverter : Converter<TMP_Dropdown.OptionData, TMP_DropdownOptionAdapter>
    {
        public TMP_DropdownOptionConverter(string label) : base(label) { }

        protected override TMP_Dropdown.OptionData ConvertWithoutAdapter(object value, UnityEngine.Object context)
            => TMP_DropdownOptionAdapter.Convert(value, true, context);

        protected override object ConvertWithoutAdapter(TMP_Dropdown.OptionData value, UnityEngine.Object context)
            => TMP_DropdownOptionAdapter.Convert(value);
    }
}