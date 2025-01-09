namespace ExampleExam
{
    public class PlayGroundRepository : IPlayGroundRepository
    {
        private int _currentId = 0;
        public PlayGroundRepository()
        {
            PlayList.Add(new PlayGround(NextId(), "Millpark", 10, 5));
            PlayList.Add(new PlayGround(NextId(), "Secret Playground", 12, 4));
            PlayList.Add(new PlayGround(NextId(), "Library", 8, 3));
            PlayList.Add(new PlayGround(NextId(), "School", 15, 7));
        }

        public List<PlayGround> PlayList { get; set; } = new List<PlayGround>();

        public List<PlayGround> GetAll() => new List<PlayGround>(PlayList);

        public PlayGround GetById(int id)
        {

            return PlayList.FirstOrDefault(x => x.Id == id)!;
        }

        public PlayGround Add(PlayGround pgr)
        {
            pgr.Id = NextId();
            PlayList.Add(pgr);
            return pgr;
        }

        public PlayGround Update(int id, PlayGround pgr)
        {
            PlayGround orig = GetById(id);
            int idx = PlayList.IndexOf(orig);
            if (orig != null)
            {
                orig = pgr;
                orig.Id = id;
                PlayList[idx] = orig;

                return orig;
            }
            return null!;
        }

        private int NextId()
        {
            return _currentId++;
        }
    }
}
