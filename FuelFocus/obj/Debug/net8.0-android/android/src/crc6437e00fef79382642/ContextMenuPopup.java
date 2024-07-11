package crc6437e00fef79382642;


public class ContextMenuPopup
	extends android.widget.PopupWindow
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("The49.Maui.ContextMenu.ContextMenuPopup, The49.Maui.ContextMenu", ContextMenuPopup.class, __md_methods);
	}


	public ContextMenuPopup ()
	{
		super ();
		if (getClass () == ContextMenuPopup.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuPopup, The49.Maui.ContextMenu", "", this, new java.lang.Object[] {  });
		}
	}


	public ContextMenuPopup (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == ContextMenuPopup.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuPopup, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2, p3 });
		}
	}


	public ContextMenuPopup (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == ContextMenuPopup.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuPopup, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}


	public ContextMenuPopup (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == ContextMenuPopup.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuPopup, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public ContextMenuPopup (android.content.Context p0)
	{
		super (p0);
		if (getClass () == ContextMenuPopup.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuPopup, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public ContextMenuPopup (android.view.View p0, int p1, int p2, boolean p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == ContextMenuPopup.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuPopup, The49.Maui.ContextMenu", "Android.Views.View, Mono.Android:System.Int32, System.Private.CoreLib:System.Int32, System.Private.CoreLib:System.Boolean, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2, p3 });
		}
	}


	public ContextMenuPopup (android.view.View p0, int p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == ContextMenuPopup.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuPopup, The49.Maui.ContextMenu", "Android.Views.View, Mono.Android:System.Int32, System.Private.CoreLib:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}


	public ContextMenuPopup (android.view.View p0)
	{
		super (p0);
		if (getClass () == ContextMenuPopup.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuPopup, The49.Maui.ContextMenu", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public ContextMenuPopup (int p0, int p1)
	{
		super (p0, p1);
		if (getClass () == ContextMenuPopup.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuPopup, The49.Maui.ContextMenu", "System.Int32, System.Private.CoreLib:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1 });
		}
	}

	public ContextMenuPopup (android.view.View p0, androidx.appcompat.view.menu.SubMenuBuilder p1, crc6437e00fef79382642.ContextMenuPopup p2)
	{
		super ();
		if (getClass () == ContextMenuPopup.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuPopup, The49.Maui.ContextMenu", "Android.Views.View, Mono.Android:AndroidX.AppCompat.View.Menu.SubMenuBuilder, Xamarin.AndroidX.AppCompat:The49.Maui.ContextMenu.ContextMenuPopup, The49.Maui.ContextMenu", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}

	public ContextMenuPopup (android.view.View p0, boolean p1)
	{
		super ();
		if (getClass () == ContextMenuPopup.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuPopup, The49.Maui.ContextMenu", "Android.Views.View, Mono.Android:System.Boolean, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1 });
		}
	}

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
