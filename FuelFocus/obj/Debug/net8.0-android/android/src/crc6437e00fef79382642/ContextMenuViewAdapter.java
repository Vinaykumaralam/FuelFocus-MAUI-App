package crc6437e00fef79382642;


public class ContextMenuViewAdapter
	extends androidx.recyclerview.widget.RecyclerView.Adapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getItemCount:()I:GetGetItemCountHandler\n" +
			"n_onBindViewHolder:(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)V:GetOnBindViewHolder_Landroidx_recyclerview_widget_RecyclerView_ViewHolder_IHandler\n" +
			"n_onViewRecycled:(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)V:GetOnViewRecycled_Landroidx_recyclerview_widget_RecyclerView_ViewHolder_Handler\n" +
			"n_onCreateViewHolder:(Landroid/view/ViewGroup;I)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;:GetOnCreateViewHolder_Landroid_view_ViewGroup_IHandler\n" +
			"";
		mono.android.Runtime.register ("The49.Maui.ContextMenu.ContextMenuViewAdapter, The49.Maui.ContextMenu", ContextMenuViewAdapter.class, __md_methods);
	}


	public ContextMenuViewAdapter ()
	{
		super ();
		if (getClass () == ContextMenuViewAdapter.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuViewAdapter, The49.Maui.ContextMenu", "", this, new java.lang.Object[] {  });
		}
	}

	public ContextMenuViewAdapter (android.content.Context p0, androidx.appcompat.view.menu.MenuBuilder p1)
	{
		super ();
		if (getClass () == ContextMenuViewAdapter.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuViewAdapter, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android:AndroidX.AppCompat.View.Menu.MenuBuilder, Xamarin.AndroidX.AppCompat", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public int getItemCount ()
	{
		return n_getItemCount ();
	}

	private native int n_getItemCount ();


	public void onBindViewHolder (androidx.recyclerview.widget.RecyclerView.ViewHolder p0, int p1)
	{
		n_onBindViewHolder (p0, p1);
	}

	private native void n_onBindViewHolder (androidx.recyclerview.widget.RecyclerView.ViewHolder p0, int p1);


	public void onViewRecycled (androidx.recyclerview.widget.RecyclerView.ViewHolder p0)
	{
		n_onViewRecycled (p0);
	}

	private native void n_onViewRecycled (androidx.recyclerview.widget.RecyclerView.ViewHolder p0);


	public androidx.recyclerview.widget.RecyclerView.ViewHolder onCreateViewHolder (android.view.ViewGroup p0, int p1)
	{
		return n_onCreateViewHolder (p0, p1);
	}

	private native androidx.recyclerview.widget.RecyclerView.ViewHolder n_onCreateViewHolder (android.view.ViewGroup p0, int p1);

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
