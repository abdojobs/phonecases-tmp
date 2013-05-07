package com.tnub.phonecases;

import java.io.IOException;
import java.io.InputStream;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.Enumeration;
import java.util.Vector;

import org.xml.sax.Parser;

import android.content.Intent;
import android.net.Uri;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.util.Log;

public class Server {
	private ServerSocket m_serverSocket;
	private InetAddress m_inetAddress;
	
	private Vector CaseCreatedListeners;
	
	
	public void AddCaseCreatedListener(CaseCreatedEvent listener)
	{
		if(CaseCreatedListeners == null)
			CaseCreatedListeners = new Vector();
		
		CaseCreatedListeners.addElement(listener);
	}
	
	private Thread m_listningThread = new Thread()
	{
		private byte buffer[] = new byte[256];
		
		@Override
		public void run()
		{
			while(!Thread.currentThread().isInterrupted())
			{
				try {

					Log.v("TNUB", "Listening for connections");
					Socket connection = m_serverSocket.accept();
					Log.v("TNUB", "Accepted connection");
					InputStream inStream = connection.getInputStream();
					
					inStream.read(buffer);
					String output = new String(buffer, "UTF-8");
					
					if(output!="" && output !=null)
					{
						int end = output.indexOf("\0");
						String result = output.substring(0, end);
						Log.v("TNUB", result);
						
						String[] resultparts = result.split("|");
						
						if(resultparts.length>0)
						{
							//New case created
							if(resultparts[0]=="00")
							{
								if(resultparts.length == 2)
								{
									if(CaseCreatedListeners!=null)
									{
										Enumeration e = CaseCreatedListeners.elements();
										while(e.hasMoreElements())
										{
											CaseCreatedEvent e1 = ((CaseCreatedEvent)e.nextElement());
											e1.CaseCreated(resultparts[1]);
										}
											
									}
										
								}
							}
							//Neglect phonecall
							else if(resultparts[0]=="01")
							{
								Log.v("TNUB", "Neglect");
							}
							//Call number
							else if(resultparts[0]=="02" && resultparts.length == 2)
							{
								Log.v("TNUB", "Call" + resultparts[1]);
							}
						}
					}
					
					connection.close();
					
				} catch (IOException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
			
		}
	};
	
	public Server(String ip)
	{
        Log.v("TNUB", "CONSTR");
		try {
			m_inetAddress = InetAddress.getByName(ip);
	        Log.v("TNUB", "Inet");
			m_serverSocket = new ServerSocket(21338);
	        Log.v("TNUB", "Socket");
			//m_serverSocket.
		} catch (UnknownHostException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}
	public void StartServer()
	{
		if(!m_listningThread.isAlive())
			m_listningThread.start();

	}
	public void StopServer()
	{
		if(m_listningThread.isAlive())
		{
			m_listningThread.interrupt();
			m_listningThread.stop();
		}
			
	}
	
	

	
}
