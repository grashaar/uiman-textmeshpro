using System;
using TMPro;

namespace UnuGames.MVVM
{
    [Serializable]
    public sealed class TMP_DropdownOptionConverter : Converter<TMP_Dropdown.OptionData, TMP_DropdownOptionAdapter>
    {
        public TMP_DropdownOptionConverter(string label) : base(label) { }

        public override TMP_Dropdown.OptionData Convert(object value, UnityEngine.Object context)
            => TMP_DropdownOptionAdapter.Convert(value, true, context);
    }
}