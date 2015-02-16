package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.Organisme;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/organismes")
@Controller
@RooWebScaffold(path = "organismes", formBackingObject = Organisme.class)
public class OrganismeController {
}
