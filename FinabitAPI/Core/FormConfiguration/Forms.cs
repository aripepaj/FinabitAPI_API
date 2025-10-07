using FinabitAPI.Core.Global.dto;

namespace FinabitAPI.Core.FormConfiguration
{
    public class Forms : BaseClass
    {
        private string? _mCaption;
        private string? _mFormType;
        private string? _mName;
        private string? _mText;

        public override string ToString()
        {
            return this._mCaption ?? "";
        }

        public string? Caption
        {
            get
            {
                return this._mCaption;
            }
            set
            {
                this._mCaption = value;
            }
        }

        public string? FormType
        {
            get
            {
                return this._mFormType;
            }
            set
            {
                this._mFormType = value;
            }
        }

        public string? Name
        {
            get
            {
                return this._mName;
            }
            set
            {
                this._mName = value;
            }
        }

        public string? Text
        {
            get
            {
                return this._mText;
            }
            set
            {
                this._mText = value;
            }
        }
    }
}