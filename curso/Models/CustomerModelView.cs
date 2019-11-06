using domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace curso.Models {
    public class CustomerModelView {
        private Customer entity;

        public CustomerModelView(Customer entity) {
            this.entity = entity;
        }

        [AllowHtml]
        public string FirstName {
            get { return this.entity.FirstName;  }
            set { entity.FirstName = value; }
        }

        public Customer Entity {  get { return entity; } }
    }
}