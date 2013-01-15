namespace LispToText.Chassis.Transform
{
    public class OpenAndCloseItem
    {
        public OpenAndCloseItem(bool isOpen, int index)
        {
            IsOpen = isOpen;
            Index = index;
        }

        public bool IsOpen { get; private set; }
        public int Index { get; private set; }
    }
}