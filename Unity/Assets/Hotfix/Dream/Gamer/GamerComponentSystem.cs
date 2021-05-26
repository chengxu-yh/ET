using System.Linq;

namespace ET
{
    public class GamerComponentAwakeSystem: AwakeSystem<GamerComponent>
    {
        public override void Awake(GamerComponent self)
        {
        }
    }
    
    public class GamerComponentDestroySystem: DestroySystem<GamerComponent>
    {
        public override void Destroy(GamerComponent self)
        {
            self.idGamers.Clear();
        }
    }
    
    public static class GamerComponentSystem
    {
        public static void Add(this GamerComponent self, Gamer gamer)
        {
            gamer.Parent = self;
            self.idGamers.Add(gamer.Id, gamer);
        }

        public static Gamer Get(this GamerComponent self, long id)
        {
            self.idGamers.TryGetValue(id, out Gamer gamer);
            return gamer;
        }

        public static void Remove(this GamerComponent self, long id)
        {
            Gamer gamer;
            self.idGamers.TryGetValue(id, out gamer);
            self.idGamers.Remove(id);
            gamer?.Dispose();
        }

        public static void RemoveNoDispose(this GamerComponent self, long id)
        {
            self.idGamers.Remove(id);
        }

        public static Gamer[] GetAll(this GamerComponent self)
        {
            return self.idGamers.Values.ToArray();
        }
    }
}