﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace donet_route.Controllers
{
    public class RouteController : Controller
    {
        private static string mappingFile = "/async/UrlMapping.js";
        private static Dictionary<String, String> mapping = null;
        public ActionResult Index()
        {

            var path = HttpContext.Request.Path;
            if(Regex.IsMatch(path,"\\.cshtml$")){
                ViewBag.viewName = path;
                return View("/Views" + path);
            }
            else
            {
                if (mapping == null)
                {
                    mapping = getUrlMapping();
                }
                if (mapping.ContainsKey(path))
                {
                    return Json(readFile(mapping[path]), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Content("Welcome to Donet !");
                }
                
                
            }

        }

        private Dictionary<String, String> getUrlMapping()
        {
            Dictionary<String, String> dic = new Dictionary<string, string>();
            String content = readFile(mappingFile);
            content = content.Replace("\r", "").Replace("\n", "").Replace(" ","");
            MatchCollection mc = Regex.Matches(content, "([\'\"])[^\'\"]+\\1\\:([\'\"])[^\'\"]+\\2");
            for (int i = 0; i < mc.Count; i++)
            {
                var arr = mc[i].ToString().Replace("'","").Replace("\"","").Split(':');
                dic.Add(arr[0], arr[1]);
            }
            return dic;
        }

        private string readFile(string file){
            string realPath = Server.MapPath(HttpContext.Request.ApplicationPath) + file;
            StringBuilder sb = new StringBuilder();
            StreamReader sr = new StreamReader(realPath, Encoding.UTF8);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
               sb.Append(line);
            }
            sr.Close();
            return sb.ToString();
        }
	}
}