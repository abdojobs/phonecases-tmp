package com.tnub.phonecases;


import java.io.IOException;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.net.UnknownHostException;
import java.sql.Date;
import java.sql.Time;
import java.text.SimpleDateFormat;

import android.annotation.SuppressLint;
import android.telephony.PhoneStateListener;
import android.telephony.TelephonyManager;
import android.text.AndroidCharacter;
import android.util.Log;


public class PhoneService extends PhoneStateListener implements CaseCreatedEvent {
	
	private String m_myNumber;
	private Socket m_socket;
	private OutputStream m_outStream;   
	private PrintWriter m_output;
	private String m_serverAddr;
	private int m_serverPort;
	private boolean m_ringing = false;
	
	private String m_currentCaseId = "";
	
	private String m_incomingNumberBuff;
	
	public PhoneService(String myNumber, String serverAddr, int serverPort)
	{
		m_myNumber = myNumber;
		
		m_serverAddr = serverAddr;
		m_serverPort = serverPort;

	}
	public void Init()
	{
		openSocket();
		//Pair with server
		m_output.println("99|" + m_myNumber + "|" + 21338);
		m_output.flush();
		
		closeSocket();
	}
	
	private void openSocket()
	{
		try {
			m_socket = new Socket(m_serverAddr,m_serverPort);
			m_outStream = m_socket.getOutputStream();    
			m_output = new PrintWriter(m_outStream);
			
		} catch (UnknownHostException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	private void closeSocket()
	{
		Log.v("TNUB", "Closing Socket");
		try {
			m_socket.close();
			m_output.close();
			m_output = null;
			m_socket = null;
		} catch (IOException e) {
			// TODO Auto-generated catch block
			Log.v("TNUB", "Exception");
			e.printStackTrace();
			
		}
		Log.v("TNUB", "Socket Closed");
	}

	@SuppressLint("SimpleDateFormat")
	@Override
    public void onCallStateChanged(int state, String incomingNumber) 
	{
		Log.v("TNUB","STATE:"+state);
		if(state == TelephonyManager.CALL_STATE_RINGING && m_ringing==false && incomingNumber != null)
		{
			m_ringing = true;
			//SEND DATA HERE
			m_incomingNumberBuff = incomingNumber;
			onIncomingCall(incomingNumber);

		}
		if(state== TelephonyManager.CALL_STATE_IDLE)
		{
			//End of call (First time idle after call_state_ringing)
			if(m_ringing)
			{
				onEndOfIncomingCall(m_incomingNumberBuff);
			}
			m_ringing = false;
		}
	}
	
	private void onIncomingCall(String incomingNumber)
	{
		openSocket();
		android.text.format.Time now = new android.text.format.Time();
		now.setToNow();
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
		String date = formatter.format(new java.util.Date());
		m_output.println("00|" + m_myNumber + "|" + incomingNumber + "|" + date);
		m_output.flush();
		/*
		Log.v("TNUB", incomingNumber);
		Log.v("TNUB", m_myNumber);
		//Crashar appen av någon anledning. vet inte när det här skulle kunna gå igenom när något är null..:S 
		*/
		closeSocket();

	}
	private void onEndOfIncomingCall(String incomingNumber)
	{
		openSocket();
		android.text.format.Time now = new android.text.format.Time();
		now.setToNow();
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
		String date = formatter.format(new java.util.Date());
		m_output.println("02|" + m_myNumber + "|" + m_currentCaseId + "|" + date);
		m_output.flush();
		closeSocket();
	}
	@Override
	public void CaseCreated(String caseId) {
		// TODO Auto-generated method stub
		m_currentCaseId = caseId;
	}
	
}