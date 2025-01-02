namespace Line98.Model
{
    public class Ball
    {
        public int colorIndex { get; set; }
        public bool isBig { get; set; }
        public Ball() { }

        public Ball(int ColorIndex, bool IsBig = true)
        {
            this.colorIndex = ColorIndex;
            this.isBig = IsBig;
        }
        public Ball Clone()
        {
            return new Ball
            {
                colorIndex = this.colorIndex,
                isBig = this.isBig
            };
        }
    }
}
