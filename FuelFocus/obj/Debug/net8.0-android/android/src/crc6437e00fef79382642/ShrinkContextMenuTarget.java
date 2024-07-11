package crc6437e00fef79382642;


public class ShrinkContextMenuTarget
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		java.lang.Runnable
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_run:()V:GetRunHandler:Java.Lang.IRunnableInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("The49.Maui.ContextMenu.ShrinkContextMenuTarget, The49.Maui.ContextMenu", ShrinkContextMenuTarget.class, __md_methods);
	}


	public ShrinkContextMenuTarget ()
	{
		super ();
		if (getClass () == ShrinkContextMenuTarget.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ShrinkContextMenuTarget, The49.Maui.ContextMenu", "", this, new java.lang.Object[] {  });
		}
	}

	public ShrinkContextMenuTarget (android.view.View p0)
	{
		super ();
		if (getClass () == ShrinkContextMenuTarget.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ShrinkContextMenuTarget, The49.Maui.ContextMenu", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public void run ()
	{
		n_run ();
	}

	private native void n_run ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
