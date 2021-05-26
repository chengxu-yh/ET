using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public class DUILoginComponentAwakeSystem : AwakeSystem<DUILoginComponent>
	{
		public override void Awake(DUILoginComponent self)
		{
			ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
			self.loginBtn = rc.Get<GameObject>("LoginBtn");
			self.loginBtn.GetComponent<Button>().onClick.AddListener(self.OnLogin);

			self.account = rc.Get<GameObject>("Account");

			self.password = rc.Get<GameObject>("Password");
		}
	}
	
	public static class DUILoginComponentSystem
	{
		public static void OnLogin(this DUILoginComponent self)
		{
			string account = self.account.GetComponent<InputField>().text;

			string password = self.password.GetComponent<InputField>().text;

			DLoginHelper.Login(self.DomainScene(), "127.0.0.1:10002", account, password).Coroutine();
		}
	}
}
