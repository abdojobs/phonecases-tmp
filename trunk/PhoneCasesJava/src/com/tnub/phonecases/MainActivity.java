package com.tnub.phonecases;

import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Bundle;
import android.app.Activity;
import android.content.Context;
import android.telephony.PhoneStateListener;
import android.telephony.TelephonyManager;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

@SuppressWarnings("unused")
public class MainActivity extends Activity {

	private boolean m_started = false;
	private Server m_server;
	private TelephonyManager m_telephony;
	private PhoneService m_phoneService; 
	
	@Override
	protected void onCreate(Bundle savedInstanceState) 
	{
		super.onCreate(savedInstanceState);
		m_telephony = (TelephonyManager)this.getSystemService(Context.TELEPHONY_SERVICE);
		setContentView(R.layout.activity_main);
		
		final Button button = (Button) findViewById(R.id.buttonStart);
		final EditText text = (EditText) findViewById(R.id.textAdress);
		final TextView IMEITextView = (TextView) findViewById(R.id.IMEITextView);
		final TextView PhoneNumberTextView = (TextView) findViewById(R.id.PhoneNumberTextView);
		
		
		
		if(m_telephony.getDeviceId() != null)
			IMEITextView.setText("IMEI: "  + m_telephony.getDeviceId());
		else
			IMEITextView.setText("IMEI: Not Available");
		
		if(m_telephony.getDeviceId() != null)
			PhoneNumberTextView.setText("MSISDN: "  + m_telephony.getLine1Number());
		else
			PhoneNumberTextView.setText("MSISDN: Not Available");
		
		
        button.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
            	m_started = !m_started;
            	if(m_started)
            	{
            		m_server = new Server(text.getText().toString());
            		m_server.StartServer();
            		startServer(text.getText().toString());
            		button.setText("Stop");
            	}
            	else
            	{
            		m_server.StopServer();
            		m_server = null;
            		m_phoneService = null;
            		button.setText("Start");
            	}
            }
        });

	}
	

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.activity_main, menu);
		return true;
	}
	//Byt namn till något smartare som beskriver funktionen bättre. Det är ingen server. Det är en listner/klient.
	private void startServer(String addr)
	{
	
		m_phoneService = new PhoneService(m_telephony.getDeviceId(),addr,21337);//"welleby.sytes.net",21337);//"192.168.0.12",21337);
		m_phoneService.Init();
		
		m_telephony.listen(m_phoneService, PhoneStateListener.LISTEN_CALL_STATE);
	}


}
