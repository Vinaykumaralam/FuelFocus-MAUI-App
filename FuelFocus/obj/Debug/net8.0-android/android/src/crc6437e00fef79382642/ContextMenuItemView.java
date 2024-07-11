package crc6437e00fef79382642;


public class ContextMenuItemView
	extends android.widget.LinearLayout
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_isEnabled:()Z:GetIsEnabledHandler\n" +
			"n_setEnabled:(Z)V:GetSetEnabled_ZHandler\n" +
			"";
		mono.android.Runtime.register ("The49.Maui.ContextMenu.ContextMenuItemView, The49.Maui.ContextMenu", ContextMenuItemView.class, __md_methods);
	}


	public ContextMenuItemView (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == ContextMenuItemView.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuItemView, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2, p3 });
		}
	}


	public ContextMenuItemView (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == ContextMenuItemView.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuItemView, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}


	public ContextMenuItemView (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == ContextMenuItemView.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuItemView, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public ContextMenuItemView (android.content.Context p0)
	{
		super (p0);
		if (getClass () == ContextMenuItemView.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuItemView, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public boolean isEnabled ()
	{
		return n_isEnabled ();
	}

	private native boolean n_isEnabled ();


	public void setEnabled (boolean p0)
	{
		n_setEnabled (p0);
	}

	private native void n_setEnabled (boolean p0);

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
