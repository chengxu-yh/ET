﻿using System.Collections.Generic;

namespace ET
{
	public class DGateSessionKeyComponent : Entity
	{
		public int alliveTime = 20000;

		private readonly Dictionary<long, string> sessionKey = new Dictionary<long, string>();
		
		public void Add(long key, string account)
		{
			this.sessionKey.Add(key, account);
			this.TimeoutRemoveKey(key).Coroutine();
		}

		public string Get(long key)
		{
			string account = null;
			this.sessionKey.TryGetValue(key, out account);
			return account;
		}

		public void Remove(long key)
		{
			this.sessionKey.Remove(key);
		}

		private async ETVoid TimeoutRemoveKey(long key)
		{
			await TimerComponent.Instance.WaitAsync(alliveTime);
			this.sessionKey.Remove(key);
		}
	}
}
