package crc6437e00fef79382642;


public class MenuActionListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnLongClickListener,
		android.view.View.OnClickListener,
		android.view.View.OnTouchListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLongClick:(Landroid/view/View;)Z:GetOnLongClick_Landroid_view_View_Handler:Android.Views.View/IOnLongClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onLongClickUseDefaultHapticFeedback:(Landroid/view/View;)Z:GetOnLongClickUseDefaultHapticFeedback_Landroid_view_View_Handler:Android.Views.View/IOnLongClickListener, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler:Android.Views.View/IOnClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onTouch:(Landroid/view/View;Landroid/view/MotionEvent;)Z:GetOnTouch_Landroid_view_View_Landroid_view_MotionEvent_Handler:Android.Views.View/IOnTouchListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("The49.Maui.ContextMenu.MenuActionListener, The49.Maui.ContextMenu", MenuActionListener.class, __md_methods);
	}


	public MenuActionListener ()
	{
		super ();
		if (getClass () == MenuActionListener.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.MenuActionListener, The49.Maui.ContextMenu", "", this, new java.lang.Object[] {  });
		}
	}


	public boolean onLongClick (android.view.View p0)
	{
		return n_onLongClick (p0);
	}

	private native boolean n_onLongClick (android.view.View p0);


	public boolean onLongClickUseDefaultHapticFeedback (android.view.View p0)
	{
		return n_onLongClickUseDefaultHapticFeedback (p0);
	}

	private native boolean n_onLongClickUseDefaultHapticFeedback (android.view.View p0);


	public void onClick (android.view.View p0)
	{
		n_onClick (p0);
	}

	private native void n_onClick (android.view.View p0);


	public boolean onTouch (android.view.View p0, android.view.MotionEvent p1)
	{
		return n_onTouch (p0, p1);
	}

	private native boolean n_onTouch (android.view.View p0, android.view.MotionEvent p1);

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