package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.MessagerieInstantaneeLiee;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/messagerieinstantaneeliees")
@Controller
@RooWebScaffold(path = "messagerieinstantaneeliees", formBackingObject = MessagerieInstantaneeLiee.class)
public class MessagerieInstantaneeLieeController {
}
