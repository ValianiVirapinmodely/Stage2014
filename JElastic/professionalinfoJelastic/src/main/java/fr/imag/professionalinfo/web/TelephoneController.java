package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.Telephone;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/telephones")
@Controller
@RooWebScaffold(path = "telephones", formBackingObject = Telephone.class)
public class TelephoneController {
}
