   M S B A M L   `   `   `  ���� \  XBrandonVanLoggerenberg_TimeKeeper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:BrandonVanLoggerenberg_TimeKeeper!BrandonVanLoggerenberg_TimeKeeper  3    -BrandonVanLoggerenberg_TimeKeeper.SetupWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Alocal/clr-namespace:BrandonVanLoggerenberg_TimeKeeper     -     � Title$  SetupWindow��$	��420��$��516.5�� � WindowStyle$
 None=�.��� $
��Left=�$	��500��.+��� -   $��SPSetupList��$	��150��$	��390��$
��Left=�$	��Top=�.+�� -   $��g_spWelcome��$��25��.+��� $��	Welcome !��� -   $��g_spConnect��$��25��.+��� $��Connectivity��� -   $��g_spUser��$��25��.+��� $��User Create��� -   $��g_spMisc��$��25��.+��� $
��Misc���� -   $��SPSetupContent��$	��350��$��Right=�$	��360��$	��Top=�.+��� -    ��Name$ 
tabControl��$	��360�� �� SelectedIndex$ 0��.@��� $��Welcome��
 � Tag$ 0��$y�	Collapsed=�.��� ���	#FFE5E5E5.+��� .���� Welcome to TimeKeeper!�� �� :8Before you can start using TimeKeeper you need to set up�� a few things.�� $��0,100,20,20q�.���� 64Note : Later on I can add the Agree or Disagree with�� 's and C's functionality here�� $��Connectivity��$ 1��$y�	Collapsed=�.��� ���	#FFE5E5E5.+��� .���� <:Would you like to connect to a SQL Server instance or work�� 
offline?�� �� ><(working offline means that other users won't be able to see�� $"if you assign appointment to them)	� -	    	�Name$ rBtn_Offline��$��Work Offline��$
��Left=�$��	5,110,0,0q�$	��Top=�$ 0��	� -
   $ rBtn_Online��$&�� Connect to a SQL Server Instance��$
��Left=�$��	5,130,0,0q�$	��Top=�$ 1���� $��Server Name :��$��	5,163,0,0q�$	��Top=�$
��Left=��� -   $��txt_ServerName��$��20��$��105,168,0,0q�$	��230��$	��Top=�$
��Left=�{�.  �� $��Database Name :��$��	5,190,0,0q�$	��Top=�$
��Left=��� -   $��txt_DatabaseName��$��20��$��105,195,0,0q�$	��230��$	��Top=�$
��Left=�{�.  �� $��Authentication :��$��	5,220,0,0q�$	��Top=�$
��Left=��� -   $��cmb_ServerAuth��$��20��$��105,225,0,0q�$	��230��$	��Top=�$
��Left=�{�.  .@��� .��Windows Authentication�� .��SQL Server Authentication�� $��
Username :��$��	5,250,0,0q�$	��Top=�$
��Left=��� -   $��txt_ServerUsername��$��20��$��105,255,0,0q�$	��230��$	��Top=�$
��Left=�{�.  �� $��
Password :��$��	5,280,0,0q�$	��Top=�$
��Left=��� -   $��txt_ServerPassword��$��20��$��105,285,0,0q�$	��230��$	��Top=�$
��Left=�{�.  �� -   $��btn_TestConnection��$��20��$	��150��$��Test Connection��$��100,310,0,0q�$	��Top=�$
��Left=��� $��Bottom=�.��:8Settings can be changed inside of TimeKeeper at any time�� $
��user��$ 2��$y�	Collapsed=�.��� ���	#FFE5E5E5.+��� .���� /-Great! Now you can create your Login Details.�� -    ��Name$ label_username��$��
Username :��$
��Left=�$��5,50,0,0q�$	��Top=��� -    ��Name$ textBox_username��$
��Left=�$��23��$��	70,55,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��200���� -   $ label_password��$��
Password :��$
��Left=�$��5,80,0,0q�$	��Top=��� -   $ textBox_password��$
��Left=�$��23��$��	70,85,0,0q�$
 Wrap=�$�� ��$	��Top=�$	��200���� -   $��btn_CreateUser��$��20��$��50��$��Create��$��215,120,0,0q�$	��Top=�$
��Left=��� $��Bottom=�.��:8Settings can be changed inside of TimeKeeper at any time�� $
��Misc��$ 3��$y�	Collapsed=�.��� ���	#FFE5E5E5.+��� .���� 97A few last things before you can finally use TimeKeeper�� -   	 ��Name$	 gbTheme��$��Theme��$
��Left=�$��5,50,0,0q�$	��Top=�$	��325��$	��150��.��� .+��� -   
 ��Name$
 	imageDark��$	��100��$	��150��$
��Left=�$��0,0,0,0q�$	��Top=�$��Images/DarkTheme.PNG���� -   $
 
imageLight��$	��100��$	��150��$
��Left=�$��	160,0,0,0q�$	��Top=�$��Images/LightTheme.PNG��	� -   $ rBtn_Theme1�� 	� 	GroupName$ ChosenTheme��$
��Dark��$
��Left=�$��
50,100,0,0q�$	��Top=�	� -   $ rBtn_Theme2��$ ChosenTheme��$��Light��$
��Left=�$��180,100,0,0q�$	��Top=� d� 	IsChecked$
 TrueY��� -   $	 
gbCalendar��$��First Day of Week��$
��Left=�$��	5,200,0,0q�$	��Top=�$	��300��$��70��.��� .+�	� -   $ rBtn_Sun��$ ChosenCalendar��$��Sunday��$
��Left=�$��	50,20,0,0q�$	��Top=�	� -   $ rBtn_Mon��$ ChosenCalendar��$��Monday��$
��Left=�$��
180,20,0,0q�$	��Top=�$
 TrueY��� $��Bottom=�.��:8Settings can be changed inside of TimeKeeper at any time� -   $��SPNav��$	��350��$��Right=�$��Bottom=�$��25��.+��� -   $��	btn_Close��$��50��$��Center=�$
��Left=�.��Close�� -    $��btn_Previous��$��50��$��Center=�$��Right=�$��	0,0,100,0q�{�.  .��
Previous�� -!   $��btn_Next��$��50��$��Right=�$��0,2,25,3q�.��Next