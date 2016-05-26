package com.yym.wechatplugin.wxapi;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;

import com.tencent.mm.sdk.openapi.BaseReq;
import com.tencent.mm.sdk.openapi.BaseResp;
import com.tencent.mm.sdk.openapi.IWXAPI;
import com.tencent.mm.sdk.openapi.IWXAPIEventHandler;
import com.tencent.mm.sdk.openapi.WXAPIFactory;
import com.yym.wechatplugin.WeChatPlugin;

import android.util.Log;

public class WXEntryActivity extends Activity implements IWXAPIEventHandler {

	private IWXAPI api;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		api = WXAPIFactory.createWXAPI(this, WeChatPlugin.appID, false);

		Log.d("ADebugTag", "onCreate wxentryactivity");
		
		Intent openMainActivity= new Intent(WXEntryActivity.this, WeChatPlugin.class);

		openMainActivity.putExtra("doterminate", "true");
		 
        openMainActivity.setFlags(Intent.FLAG_ACTIVITY_REORDER_TO_FRONT);
        startActivity(openMainActivity);
		finish();
	}

	@Override
	protected void onNewIntent(Intent intent) {
		super.onNewIntent(intent);
		Log.d("ADebugTag", "onNewIntent wxentryactivity");
		
		setIntent(intent);
        api.handleIntent(intent, this);
	}

	@Override
	public void onReq(BaseReq req) {
	}

	@Override
	public void onResp(BaseResp resp) {
		String result = null;
		switch (resp.errCode) {
			case BaseResp.ErrCode.ERR_OK: {
				result = "分享成功";
			}
			break;
			case BaseResp.ErrCode.ERR_USER_CANCEL:
				result = "分享取消";
				break;
			case BaseResp.ErrCode.ERR_AUTH_DENIED:
				result = "分享被拒绝";
				break;
			default:
				result = "分享返回";
				break;
		}

		//Toast.makeText(this, result, Toast.LENGTH_LONG).show();
		Log.d("ADebugTag", "onResp wxentryactivity");
	}
}