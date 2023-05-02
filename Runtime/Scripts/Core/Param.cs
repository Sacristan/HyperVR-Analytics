using System;

namespace HyperVR.Analytics
{
    public class Param
    {
        public string Name { get; private set; }

        public bool HasAnyValue => HasStringValue || HasLongValue || HasDoubleValue || HasBoolValue;

        public bool HasStringValue { get; private set; }
        public string StringValue { get; private set; }

        public bool HasLongValue { get; private set; }
        public long LongValue { get; private set; }

        public bool HasDoubleValue { get; private set; }
        public double DoubleValue { get; private set; }

        public bool HasBoolValue { get; private set; }
        public bool BoolValue { get; private set; }

        public object ObjectValue
        {
            get
            {
                if (HasStringValue) return StringValue;
                if (HasLongValue) return LongValue;
                if (HasDoubleValue) return DoubleValue;
                if (HasBoolValue) return BoolValue.ToString();

                throw new Exception($"Param '{Name}' has no value");
            }
        }

        /// Use Analytics.GetParam() api to get new instance of Param 
        internal Param()
        {
        }

        private void Reset()
        {
            HasStringValue = false;
            HasLongValue = false;
            HasDoubleValue = false;
            HasBoolValue = false;
        }

        public Param Set(string name, string value)
        {
            Reset();
            Name = name;
            HasStringValue = true;
            StringValue = value;
            return this;
        }

        public Param Set(string name, long value)
        {
            Reset();
            Name = name;
            HasLongValue = true;
            LongValue = value;
            return this;
        }

        public Param Set(string name, float value)
        {
            Reset();
            Name = name;
            HasDoubleValue = true;
            DoubleValue = value;
            return this;
        }

        public Param Set(string name, bool value)
        {
            Reset();
            Name = name;
            HasBoolValue = true;
            BoolValue = value;
            return this;
        }

        public override string ToString()
        {
            if (HasStringValue)
            {
                return $"'{Name}: {StringValue}' {"(string)".Colorize("grey")}";
            }

            if (HasLongValue)
            {
                return $"'{Name}: {LongValue}' {"(long)".Colorize("grey")}";
            }

            if (HasDoubleValue)
            {
                return $"'{Name}: {DoubleValue}' {"(double)".Colorize("grey")}";
            }

            if (HasBoolValue)
            {
                return $"'{Name}: {BoolValue}' {"(bool)".Colorize("grey")}";
            }

            return "NULL value";
        }
    }
}