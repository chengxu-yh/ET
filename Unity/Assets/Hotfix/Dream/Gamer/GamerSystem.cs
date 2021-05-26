namespace ET
{
    public class GamerSystem: AwakeSystem<Gamer, string>
    {
        public override void Awake(Gamer self, string namer)
        {
            self.Namer = namer;
        }
    }
}