package crc6437e00fef79382642;


public class ContextMenuBackgroundView
	extends android.widget.FrameLayout
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAttachedToWindow:()V:GetOnAttachedToWindowHandler\n" +
			"n_onTouchEvent:(Landroid/view/MotionEvent;)Z:GetOnTouchEvent_Landroid_view_MotionEvent_Handler\n" +
			"";
		mono.android.Runtime.register ("The49.Maui.ContextMenu.ContextMenuBackgroundView, The49.Maui.ContextMenu", ContextMenuBackgroundView.class, __md_methods);
	}


	public ContextMenuBackgroundView (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == ContextMenuBackgroundView.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuBackgroundView, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2, p3 });
		}
	}


	public ContextMenuBackgroundView (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == ContextMenuBackgroundView.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuBackgroundView, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}


	public ContextMenuBackgroundView (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == ContextMenuBackgroundView.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuBackgroundView, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public ContextMenuBackgroundView (android.content.Context p0)
	{
		super (p0);
		if (getClass () == ContextMenuBackgroundView.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuBackgroundView, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public void onAttachedToWindow ()
	{
		n_onAttachedToWindow ();
	}

	private native void n_onAttachedToWindow ();


	public boolean onTouchEvent (android.view.MotionEvent p0)
	{
		return n_onTouchEvent (p0);
	}

	private native boolean n_onTouchEvent (android.view.MotionEvent p0);

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
