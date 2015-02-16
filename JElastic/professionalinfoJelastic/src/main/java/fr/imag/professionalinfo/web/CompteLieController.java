package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.CompteLie;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/comptelies")
@Controller
@RooWebScaffold(path = "comptelies", formBackingObject = CompteLie.class)
public class CompteLieController {
}
