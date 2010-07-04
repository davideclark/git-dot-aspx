#region License

// Copyright 2010 Jeremy Skinner (http://www.jeremyskinner.co.uk)
//  
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://github.com/JeremySkinner/git-dot-aspx

#endregion

namespace GitAspx {
	using System;
	using System.ComponentModel;
	using System.Web.Mvc;
	using System.Web.Routing;

	public static class Helpers {
		public static string ProjectUrl(this UrlHelper urlHelper, string project) {
			return urlHelper.RouteUrl("project", new RouteValueDictionary(new {project}),
			                          urlHelper.RequestContext.HttpContext.Request.Url.Scheme,
			                          urlHelper.RequestContext.HttpContext.Request.Url.Host);
		}

		public static string GetDescription(this Enum e) {
			var field = e.GetType().GetField(e.ToString());
			var attributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof (DescriptionAttribute), false);

			if (attributes.Length > 0) {
				return attributes[0].Description;
			}

			return e.ToString();
		}

		public static string ToPrettyDateString(this DateTime d) {
			TimeSpan s = DateTime.Now.Subtract(d);
			int dayDiff = (int)s.TotalDays;
			int secDiff = (int)s.TotalSeconds;

			if (dayDiff < 0 || dayDiff >= 31)
				return string.Format("{0:MMMM d, yyyy}", d);

			if (dayDiff == 0) {
				if (secDiff < 60)
					return "just now";

				if (secDiff < 120)
					return "1 minute ago";

				if (secDiff < 3600)
					return string.Format("{0} minutes ago", Math.Floor((double)secDiff / 60));

				if (secDiff < 7200)
					return "1 hour ago";

				if (secDiff < 86400)
					return string.Format("{0} hours ago", Math.Floor((double)secDiff / 3600));
			}

			if (dayDiff == 1)
				return "yesterday";

			if (dayDiff < 7)
				return string.Format("{0} days ago", dayDiff);

			if (dayDiff < 31)
				return string.Format("{0} weeks ago", Math.Ceiling((double)dayDiff / 7));

			return null;
		}
	}
}