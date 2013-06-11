namespace SimpleSQL.Domain
{
    public class InnerJoin
    {
        public string WithTable { get; set; }
        public string FromAttribute { get; set; }
        public string ToAttribute { get; set; }

        public string SingleNameFrom
        {
            get
            {
                return this.FromAttribute.Contains(".") ? this.FromAttribute.Split('.')[1] : this.FromAttribute;
            }
        }

        public string SingleNameTo
        {
            get
            {
                return this.ToAttribute.Contains(".") ? this.ToAttribute.Split('.')[1] : this.ToAttribute;
            }
        }
    }
}