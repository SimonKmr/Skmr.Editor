using Skmr.Editor.Analyzer.ComputerVision;


namespace Skmr.Editor.Director
{
    public class FrameInfo
    {
        Position[][] Positions { get; set; }
        public FrameInfo(Position[][] positions)
        {
            Positions = positions;
        }

        public FrameInfo(string folder)
        {
            ImageLoader imageLoader = new ImageLoader(folder);
            List<Position[]> screenshotInfo = new List<Position[]>();
            foreach(var screenshotImage in imageLoader.LazyLoad())
            {
                screenshotInfo.Add(LostArk.GetHealthbarPositions(screenshotImage));
            }
            Positions = screenshotInfo.ToArray();
        }

        public int[] ToTimepoints()
        {
            List<int> timepoints = new List<int>();
            for (int i = 0; i < Positions.Length; i++)
            {
                if (Positions[i].Length > 0) timepoints.Add(i);
            }
            return timepoints.ToArray();
        }
        public Timeline ToTimeline()
        {
            int[] timeline = new int[Positions.Length];
            for (int i = 0; i < Positions.Length; i++) timeline[i] = Positions[i].Length;
            return new Timeline(timeline);
        }
    }
}
