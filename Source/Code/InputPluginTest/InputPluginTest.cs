using System;
using System.Linq;
using Duality.Input;
using NUnit.Framework;
using static MFEP.Duality.Plugins.InputPlugin.InputManager;
using static MFEP.Duality.Plugins.InputPlugin.KeyRole;

namespace MFEP.Duality.Plugins.InputPlugin.Test
{
	[TestFixture]
	public class InputPluginTest
	{
		[SetUp]
		public void SetUp ()
		{
			RegisterButton (new ButtonTuple ("Up", new[] { (KeyValue)Key.Up, (KeyValue)MouseButton.Left }));
			RegisterButton (new ButtonTuple ("Down", new[] { (KeyValue)Key.Down, (KeyValue)MouseButton.Right }));
		}

		[TearDown]
		public void TearDown ()
		{
			RemoveButton ("Up");
			RemoveButton ("Down");
			RemoveButton ("Left");
			RemoveButton ("Button0");
		}

		[Test]
		public void AddToButtonTest ()
		{
			Assert.IsFalse (AddToButton ("Nonsense", Key.Up));
			Assert.IsFalse (AddToButton ("Up", Key.Up));
			Assert.IsFalse (AddToButton ("Up", Key.Up, Negative));
			Assert.IsTrue (AddToButton ("Up", Key.A));
			Assert.IsTrue (AddToButton ("Up", MouseButton.Extra2));
			Assert.IsTrue (AddToButton ("Up", (KeyValue)Key.Escape));
			Assert.IsTrue (AddToButton ("Up", Key.Down, Negative));
			Assert.AreEqual (GetKeysOfButton("Up", Positive).Select(vb => vb.Index),
				new[] { (int)Key.Up, (int)MouseButton.Left, (int)Key.A, (int)MouseButton.Extra2, (int)Key.Escape });
			Assert.AreEqual (GetKeysOfButton("Up", Negative).Select(vb => vb.Index),
				new[] { (int)Key.Down });
		}

		[Test]
		public void HitPressedReleasedTest ()
		{
			Assert.IsFalse (IsButtonHit ("Up"));
			Assert.Throws<ArgumentException> (() => { IsButtonHit ("Nonsense"); });
			Assert.IsFalse (IsButtonPressed ("Down"));
			Assert.Throws<ArgumentException> (() => { IsButtonPressed ("Nonsense"); });
			Assert.IsFalse (IsButtonReleased ("Down"));
			Assert.Throws<ArgumentException> (() => { IsButtonReleased ("Nonsense"); });
		}

		[Test]
		public void GetTest ()
		{
			Assert.AreEqual (GetAxis ("Up"), 0.0f);
		}

		[Test]
		public void RegisterButtonTest ()
		{
			RegisterButton ();
			Assert.IsFalse (IsButtonPressed ("Button0"));
			Assert.IsTrue (RegisterButton (new ButtonTuple ("Left", new[] { (KeyValue)Key.Left, (KeyValue)Key.Right })));
			Assert.IsFalse (IsButtonPressed ("Left"));
		}

		[Test]
		public void RemoveButtonTest ()
		{
			Assert.IsFalse (RemoveButton ("Nonsense"));
			Assert.IsTrue (RemoveButton ("Up"));
			Assert.Throws<ArgumentException> (() => { IsButtonPressed ("Up"); });
		}

		[Test]
		public void RemoveFromButtonTest ()
		{
			Assert.IsFalse (RemoveFromButton ("Nonsense", Key.Escape));
			Assert.IsFalse (RemoveFromButton ("Nonsense", MouseButton.Extra1));
			Assert.IsFalse (RemoveFromButton ("Nonsense", (KeyValue)Key.Up));

			Assert.IsFalse (RemoveFromButton ("Up", Key.Escape));
			Assert.IsFalse (RemoveFromButton ("Up", MouseButton.Extra4));
			Assert.IsFalse (RemoveFromButton ("Up", (KeyValue)MouseButton.Extra4));

			Assert.IsTrue (RemoveFromButton ("Up", Key.Up));
			Assert.IsTrue (RemoveFromButton ("Up", MouseButton.Left));
			Assert.IsTrue (RemoveFromButton ("Down", (KeyValue)Key.Down));
		}

		[Test]
		public void RenameButtonTest ()
		{
			Assert.IsFalse (RenameButton ("Nonsense", "CommonSense"));
			Assert.IsFalse (RenameButton ("Up", "Down"));
			Assert.IsTrue (RenameButton ("Up", "Left"));
			Assert.Throws<ArgumentException> (() => { IsButtonPressed ("Up"); });
			Assert.IsFalse (IsButtonReleased ("Left"));
		}
	}
}