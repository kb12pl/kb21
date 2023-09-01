win:short('F1',"kb.sys_dialog('sys_code')")
win:short('F8', "kb.sys_scripts():run_list()")
--win:short('F9', "	kb.sys_testy=nil 	kb.sys_testy()")
--win:short('F12', "ok(123)")



win.ptr:AppendMenu({parent="",name="System"})
win.ptr:AppendMenu({parent="System",name="Exit"})

win.ptr:AppendMenu({parent="",name="Konfiguracja"})
win.ptr:AppendMenu({parent="Konfiguracja",name="Osoby"})
win.ptr:AppendMenu({parent="Konfiguracja",name="Drzwi"})
win.ptr:AppendMenu({parent="Konfiguracja",name="Uprawnienia"})

win.ptr:AppendMenu({parent="",name="Drzwi"})
win.ptr:AppendMenu({parent="Drzwi",name="We-wy główne"})
win.ptr:AppendMenu({parent="Drzwi",name="We-wy hala"})
win.ptr:AppendMenu({parent="Drzwi",name="Wszystkie zdarzenia"})

win.ptr:AppendMenu({parent="",name="Pracownicy"})
win.ptr:AppendMenu({parent="Pracownicy",name="We-wy główne wg okresu"})
win.ptr:AppendMenu({parent="Pracownicy",name="We-wy hala wg okresu"})


win.event_lock=false


function win:onMenu(menu)
    if menu=='Wszystkie zdarzenia' then
        kb.sys_page('rcp_zdarzenia')
    elseif menu=='Osoby' then
        kb.sys_page('rcp_osoby')    
    elseif menu=='Drzwi' then
        kb.sys_page('rcp_drzwi')  
    elseif menu=='Uprawnienia' then
        kb.sys_page('rcp_uprawnienia')  
    elseif menu=='We-wy główne' then
        kb.sys_page('rcp_glowne')
    elseif menu=='We-wy hala' then
        kb.sys_page('rcp_hala')
    elseif menu=='We-wy główne wg okresu' then
        kb.sys_page('rcp_glowne_mies')
    elseif menu=='We-wy hala wg okresu' then
        kb.sys_page('rcp_hala_mies')
    elseif menu=='Exit' then
        win:exitFrame()
    end
    
end


