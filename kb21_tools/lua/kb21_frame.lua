--win:short('F1',"kb.sys_dialog('sys_code')")
win:short('F8', "kb.sys_scripts():run_list()")
--win:short('F9', "	kb.sys_testy=nil 	kb.sys_testy()")
--win:short('F12', "ok(123)")



win.ptr:AppendMenu({parent="",name="System"})
win.ptr:AppendMenu({parent="System",name="Exit"})

win.ptr:AppendMenu({parent="",name="Konfiguracja"})
win.ptr:AppendMenu({parent="Konfiguracja",name="Karty"})
win.ptr:AppendMenu({parent="Konfiguracja",name="Drzwi"})
win.ptr:AppendMenu({parent="Konfiguracja",name="Zdarzenia"})

win.ptr:AppendMenu({parent="",name="Raporty"})
win.ptr:AppendMenu({parent="Raporty",name="Wejście główne"})
win.ptr:AppendMenu({parent="Raporty",name="Wejście na produkcję"})

win:short('F1',"win:onMenu('Karty')")

win.event_lock=false


function win:onMenu(menu)
    if menu=='Zdarzenia' then
        kb.sys_page('rcp_zdarzenia')
    elseif menu=='Karty' then
        kb.sys_page('rcp_karty')
    elseif menu=='Drzwi' then
        kb.sys_page('rcp_drzwi')  
    elseif menu=='Wejście główne' then
        kb.sys_page('rcp_glowne')
    elseif menu=='Wejście na produkcję' then
        kb.sys_page('rcp_produkcja')
    end
    
end


