using UnityEngine;
using TMPro;

namespace UnuGames.MVVM
{
    using OptionData = TMP_Dropdown.OptionData;

    [CreateAssetMenu(menuName = "UIMan/Adapters/TMP Dropdown Option Adapter")]
    public class TMP_DropdownOptionAdapter : Adapter<OptionData>
    {
        [SerializeField]
        private bool forceToString = true;

        private static readonly OptionData _defaultValue = new OptionData(string.Empty);

        public override OptionData Convert(object value, Object context)
            => Convert(value, this.forceToString, context);

        public static OptionData Convert(object value, bool forceToString, Object context)
        {
            if (value == null)
                return _defaultValue;

            if (!(value is OptionData val))
            {
                if (forceToString)
                {
                    val = new OptionData(value.ToString());
                }
                else
                {
                    UnuLogger.LogError($"Cannot convert '{value}' to {nameof(TMP_Dropdown)}.{nameof(OptionData)}.", context);
                    val = _defaultValue;
                }
            }

            return val;
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("UIMan/Adapters/TMP Dropdown Option Adapter")]
        private static void CreateTMPDropdownOptionAdapterAsset()
            => CreateAdapter<TMP_DropdownOptionAdapter>(nameof(TMP_DropdownOptionAdapter));
#endif
    }
}