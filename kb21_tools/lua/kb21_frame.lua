win:short('F1',"kb.sys_dialog('sys_code')")
win:short('F8', "kb.sys_scripts():run_list()")
--win:short('F9', "	kb.sys_testy=nil 	kb.sys_testy()")
--win:short('F12', "ok(123)")



win.ptr:AppendMenu({parent="",name="System"})
win.ptr:AppendMenu({parent="System",name="Exit"})

win.ptr:AppendMenu({parent="",name="Konfiguracja"})
win.ptr:AppendMenu({parent="Konfiguracja",name="Karty"})
win.ptr:AppendMenu({parent="Konfiguracja",name="Drzwi"})

win.ptr:AppendMenu({parent="",name="Drzwi"})
win.ptr:AppendMenu({parent="Drzwi",name="We-wy główne"})
win.ptr:AppendMenu({parent="Drzwi",name="We-wy na produkcję"})
win.ptr:AppendMenu({parent="Drzwi",name="Wszystkie zdarzenia"})

win.ptr:AppendMenu({parent="",name="Pracownicy"})
win.ptr:AppendMenu({parent="Pracownicy",name="Miesięcznie we-wy główne"})
win.ptr:AppendMenu({parent="Pracownicy",name="Miesięcznie we-wy na produkcję"})


win.event_lock=false


function win:onMenu(menu)
    if menu=='Wszystkie zdarzenia' then
        kb.sys_page('rcp_zdarzenia')
    elseif menu=='Karty' then
        kb.sys_page('rcp_karty')
    elseif menu=='Drzwi' then
        kb.sys_page('rcp_drzwi')  
    elseif menu=='We-wy główne' then
        kb.sys_page('rcp_glowne')
    elseif menu=='We-wy na produkcję' then
        kb.sys_page('rcp_produkcja')
    elseif menu=='Miesięcznie we-wy główne' then
        kb.sys_page('rcp_glowns_meisiecznie')
    elseif menu=='Miesięcznie we-wy na produkcję' then
        kb.sys_page('rcp_produkcja_miesiecznie')
    end
    
end


