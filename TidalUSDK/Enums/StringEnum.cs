namespace TidalUSDK.Enums {
    public abstract class StringEnum {
        public string Value { get; }
        
        protected StringEnum(string val) {
            this.Value = val;
        }

        public override string ToString() => this.Value;
    }
}