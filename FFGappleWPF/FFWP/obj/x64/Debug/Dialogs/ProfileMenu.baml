   M S B A M L   `   `   `  ����?  ;FFWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FFWP.DialogsFFWP.Dialogs  5          FFWP.Dialogs.ProfileMenu   5      M IModernWpf.Controls, Version=0.9.6.0, Culture=neutral, PublicKeyToken=nullD @ModernWpf, Version=0.9.6.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:FFWP.Dialogs     5      V RGu.Wpf.Adorners, Version=2.1.1.0, Culture=neutral, PublicKeyToken=6dfb64f9972bd31d$adornershttp://gu.se/Adorners  5      &   ModernWpf.Controls.ContentDialog    Title$  ProfileMenu��5
      
�������
���   �.��5      � $	��247��6   $	��497��6    � ColumnDefinitions 5   
   �� 5      $
��365* �6   �� 5      $
��132* �6   .+�5   
   � $��0,0,3,0q�6   $��2��6     5      �� 5      $
��220* �6#   �� 5      $��161.022 �6#   �� 5      $
��113* �6#    � RowDefinitions 5      �� 5      �� 5      $��52 �6    .+�5      �� $��0,0,-1,0q�6   $��2��6'   $��3��68   .+�5      ��  �� Left$	 276��6   $
��Wrap=�6.   
 �� Top$ 14��6B   $��18(�6R   $
��Bold"�6`   $��Center=�6r   $	��Top=�6�   �� 6�    � Language$ tr-tr�6�   $�Fabric Minecraft: ��6�   6�   �� 5      $	 276��6   $
��Wrap=�6.   $ 50��6B   $��18(�6R   $
��Bold"�6`   $��Center=�6r   $	��Top=�6�   �� 6�   $ tr-tr�6�   $�Forge Minecraft:��6�   6�   %  ModernWpf.Controls.ToggleSwitch  5      -   5          Name$ 	FabricTOG��5   "   $��44��65   $	 441��6@   $ 12��6R   $	��Top=�6b   
���   �$��Center=�6�   6�     5      -   5        $ ForgeTOG��5   "   $��44��64   $	 441��6?   $ 46��6Q   $	��Top=�6a   $��Center=�6y   6�   �� 5       -   5         ��Name$ vercombo��5       $	 278��6-   $	 159��6?   $	��209��6P   $��Center=�6\   6y   �� 5!      -   5         ��Name$ addbtt��5!      $��Save Profile��6)   $ 10��6@   $	 205��6Q   $	��477��6b   ��5"      �� 5#      	 �� EndPoint$	 0.5,1&�6.   
 �� 
StartPoint$
 0.5,0&�6?   .��5$      �  � Color$ Black��6+   � 5%      $ 	#FF5D17DE��6+    � Offset$ 0Q�6=   5&      5(      �� 5)      $	 278��6   $ 78��6.   $��3��6>   $	��209��6I   $��Center=�6U   $��Center=�6r   �� 5*      -   5         ��Name$ 	developer��5*      $
��Wrap=�6/   $��14(�6C   $
��Bold"�6Q   $	��210��6c   $
��Left=�6o   $	 280��6�   $ 88.19��6�   $	��Top=�6�   �� 6�   $�	Developer��6�   �� 6�   $ de-de�6�   $�:��6�   6   �� 5+      -   5        $
 date��5+      $
��Wrap=�6*   $��14(�6>   $
��Bold"�6L   $	��210��6^   $
��Left=�6j   $	 280��6�   $	 111��6�   $	��Top=�6�   �� 6�   $
�Date��6�   �� 6�   $ de-de�6�   $�:��6�   6�   �� 5,      $
��Wrap=�6   $��14(�60   $
��Bold"�6>   $	��210��6P   $	 280��6\   $	 133��6n   $��Center=�6   $	��Top=�6�   �� 6�   $�	Publisher��6�   �� 6�   $ de-de�6�   $�:��6�   6�   5-      �� 5.      -   5         ��Name$ profilename��5.      $��Hidden=�6+   $��Visible=�6_   $
��Left=�6�   $��	10,10,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $��Profil Ismi:��6�   $	��Top=�6�   $	��109��6   {�.  �� 
IsReadOnly . $y�Hidden=�6/  �� 5/      -   5        $ username��5/      $
��Left=�6(   $��	10,47,0,0q�6C   $
 Wrap=�6V   $��Kullanici Adi:��6j   $	��Top=�6�   $	��228��6�   $��2��6�   �� 50      -	   5        $ 	serveript��50      $
��Left=�6)   $��	10,84,0,0q�6D   $
 Wrap=�6W   $��	Server IP��6k   $	��Top=�6|   $	��159��6�   �� 51      -
   5        $ serverportt��51      $
��Left=�6+   $��
174,84,0,0q�6F   $
 Wrap=�6Z   $
��Port��6n   $	��Top=�6z   $��44��6�   $��2��6�   �� 52      -   5        $ widthtxt��52      $
��Left=�6(   $��
10,120,0,0q�6C   $
 Wrap=�6W   $��Genislik��6k   $	��Top=�6{   $	��114��6�   $��2��6�   �� 53      -   5        $ 	heigthtxt��53      $
��Left=�6)   $��124,120,0,0q�6D   $
 Wrap=�6Y   $��
Yükseklik��6m   $	��Top=�6~   $	��114��6�   $��2��6�   $��2��6�   �� 54      -   5        $ 	customdir��54      $
��Left=�6)   $��
10,157,0,0q�6D   $
 Wrap=�6X   $��Özel Dizin��6l   $	��Top=�6~   $	��159��6�   �� 55      $��..��6   $��174,157,0,0q�6"   $��32��67   $��64��6C   
�������$��14(�6a   $
��Bold"�6o   $��2��6�   $	��Top=�6�   ��56      �� 57      $	 0.5,1&�6*   $
 0.5,0&�6;   .��58      � $ Black��6'   � 59      $ 	#FF5D17DE��6'   $ 0Q�69   5:      5<      �� 5>      -   5        $
 mail��5>      $
��Left=�6$   $��	10,47,0,0q�6?   $
 Wrap=�6R   $��E-Mail��6f   $	��Top=�6t   $	��114��6�   �� 5?      $
��Left=�6   $��	20,57,0,0q�63   $��1��6F   $
��Wrap=�6S   $��FFNet��6g   $	��Top=�6t   A� 5@      -   5         A�Name$ profilnameerror��5@      $
��Left=�6/   $��16��6J   
���   �$	��Top=�6e   $��17��6}   $��	-9,18,0,0q�6�   $y�Hidden=�6�   ��5A      �� 5B      $	 0.5,1&�6*   $
 0.5,0&�6;   .��5C      � $ Black��6'   � 5D      $ 	#FF5D17DE��6'   $ 0Q�69   5E      5G      A� 5H      -   5        $ 	crederror��5H      $
��Left=�6)   $��16��6D   
���   �$	��Top=�6_   $��17��6w   $��	-9,55,0,0q�6�   $y�Hidden=�6�   ��5I      �� 5J      $	 0.5,1&�6*   $
 0.5,0&�6;   .��5K      � $ Black��6'   � 5L      $ 	#FF5D17DE��6'   $ 0Q�69   5M      5O      A� 5Q      -   5        $ 
mcvererror��5Q      $
��Left=�6*   $��16��6E   
���   �$	��Top=�6`   $��17��6x   $��106,128,0,0q�6�   $��2��6�   $y�Hidden=�6�   ��5R      �� 5S      $	 0.5,1&�6*   $
 0.5,0&�6;   .��5T      � $ Black��6'   � 5U      $ 	#FF5D17DE��6'   $ 0Q�69   5V      5X      �� 5Y      -   5        $ ReadyVersion��5Y      $��Hidden=�6,   $��Visible=�6`   $
��Left=�6�   $��	10,10,0,0q�6�   $
 Wrap=�6�   $��rV��6�   $	��Top=�6�   $	��228��6�   {�. $��2��6  $��10000000000��6(  $��1000000000000��6?   . $��32��6k  5Z      G� 5`   
   -   5         G�Name$ passtxt��5`      $	��114��6(     Gu.Wpf.Adorners.Watermark   Text$  ��64   $��126,47,125,168q�6O   �� 5a   
   -   5        $ 
passholder��5a      $
��Left=�6(   $��
131,53,0,0q�6C   $
��Wrap=�6W   $	��Top=�6k   $��14(�6�   $y�Hidden=�6�   �� 6�   $ de-de�6�   $�Sifre��6�   6�   �� 5b   
   -   5        $ devdata��5b      $
��Left=�6%   $��
359,88,0,0q�6@   $
��Wrap=�6T   $	��Top=�6h   $��14(�6�   $��2��6�   �� 6�   $ de-de�6�   $�?!��6�   6�   �� 5c   
   -   5        $ datedata��5c      $
��Left=�6&   $��359,111,0,0q�6A   $
��Wrap=�6V   $	��Top=�6j   $��14(�6�   $��2��6�   �� 6�   $ de-de�6�   $�?!��6�   6�   �� 5d   
   -   5        $ pubdata��5d      $
��Left=�6%   $��359,133,0,0q�6@   $
��Wrap=�6U   $	��Top=�6i   $��14(�6�   $��2��6�   �� 6�   $ de-de�6�   $�?!��6�   6�   �� 5e   
   -   5        $��X��5e      $��
85,-37,0,0q�6   $	��Top=�61   $��44��6I   
�������$��1��6g   6w   5n      5q      