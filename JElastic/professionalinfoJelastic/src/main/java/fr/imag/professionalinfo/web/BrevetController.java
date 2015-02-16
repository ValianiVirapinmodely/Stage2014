package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.Brevet;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/brevets")
@Controller
@RooWebScaffold(path = "brevets", formBackingObject = Brevet.class)
public class BrevetController {
}
