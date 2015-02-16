package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.Certification;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/certifications")
@Controller
@RooWebScaffold(path = "certifications", formBackingObject = Certification.class)
public class CertificationController {
}
