   M S B A M L   `   `   `  ���� \  XBrandonVanLoggerenberg_TimeKeeper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:BrandonVanLoggerenberg_TimeKeeper!BrandonVanLoggerenberg_TimeKeeper  =    7BrandonVanLoggerenberg_TimeKeeper.Calendar_Appointments   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Alocal/clr-namespace:BrandonVanLoggerenberg_TimeKeeper     -     � Title$  Appointments��$	��421��$	��630�� � 
ResizeMode$ NoResize=�.��� -   $��MainGrid��$	��630��$��0,0,-8,0q�$
��Left=�$	��Top=�$	��384��.+�� -   $��AppointmentsHeader��$	��630��$��25��$
��Left=�$	��Top=�$��0,0,0,0q�.+��� -   $��AppointmentsHeaderLabel��$
��test��$��292,0,188,1q�$	��150��� $	��400��$��0,25,0,0q�$
��Left=�$	��Top=�.+��� -   $��stackpanel_TimeLineBorder��$��
-20,10,0,0q�
���   �$��1q� �� CornerRadius$ 1��$	��350��$	��335��._��� ��. .���� -   $��Stackpanel_TimeLine��$
��Left=�$	��Top=�$	��335�� �� Orientation$ Vertical=�$	��350��� -   $ ��AppointmentsHeader_Details��$	��210��$��25��$
��Left=�$	��Top=�$��
398,25,0,0q�.+��� -   $%��AppointmentsHeader_DetailsLabel��$
��test��$	��150��� -	   $��GridAppointmentDetails��$��
398,50,0,0q�$
��Left=�$	��Top=�$	��210��$	��335��.+��� -
    ��Name$ label_AppointmentHeader��$��	44,-3,0,0q�$��Appointment Header :��$
��Left=�$	��Top=��� -    ��Name$ textBox_AppointmentHeader��$
��Left=�$��23��$��	11,22,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��191���� -   $" label_AppointmentDescription��$��	35,41,0,0q�$��Appointment Description :��$
��Left=�$	��Top=��� -   $$ textBox_AppointmentDescription�� �� AcceptsReturn .  �� 
AcceptsTab . $
 Wrap=�	 �� 	IsEnabled	 . $
��Left=�$��97��$��	10,65,0,0q�$	��Top=�$	��191���� -   $ label_Attendees��$��
70,158,0,0q�$��Attendees :��$
��Left=�$	��Top=�� $	��200��$��	5,180,0,0q�$
��Left=�$	��Top=�.+��� -   $ ��stackpanel_AttendeesBorder��
���   �$��1q�$ 1��._��� ��. .���� -   $��Stackpanel_Attendees��$
��Left=�$	��Top=�$��90��$ Vertical=�$	��190���� -   $��button_Save��$��152,301,0,0q�$
��Left=�$	��Top=�$��30��$��50��.��Save�� -   $��button_Cancel��$��
10,300,0,0q�$
��Left=�$	��Top=�$��30��$��50��.��Cancel