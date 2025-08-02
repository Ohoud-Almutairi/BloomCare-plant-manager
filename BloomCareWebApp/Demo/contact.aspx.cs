using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Drawing;
using System.IO;
using Ohoud10108WebApp.App_Code; // Assuming CRUD.cs is here

namespace Ohoud10108WebApp.Demo
{
    public partial class contact : System.Web.UI.Page
    {
            protected void Page_Load(object sender, EventArgs e)
            {

            }
            protected void btnSendMailViaMailMgr_Click(object sender, EventArgs e)
            {
                sendEmail2();
            }
            private void InsertContactUs()
            {
                CRUD myCrud = new CRUD();
                string mySql = @"Insert contactus(senderEmail,subject, message) 
                        values (@senderEmail,@subject,@message)";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@senderEmail", txtSenderEmail.Text);
                myPara.Add("@subject", txtSubject.Text);
                myPara.Add("@message", txtBody.Text);
                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                if (rtn >= 1)
                {
                    lblMsg.Text = "Email log Successed";
                    lblMsg.ForeColor = Color.Green;
                }
                else
                {
                    lblMsg.Text = "Email log failed";
                    lblMsg.ForeColor = Color.Red;
                }
            }

            /// <summary>
            /// updated on Oct 30,2020
            /// 
            /// to do :create a class to insert attached files to db and another one to inert attached files to folder
            /// </summary>
            protected void sendEmail2()
            {
                //int intSenderId = 111;
                string senderName = Page.User.Identity.Name;
                senderName = (string.IsNullOrWhiteSpace(senderName)) ? "Admin" : Page.User.Identity.Name;
                string rtn = "";
                if (string.IsNullOrEmpty(txtSubject.Text) || string.IsNullOrEmpty(txtBody.Text))
                {
                    lblMsg.Text = "Please fill Subject & email body";
                    lblMsg.ForeColor = Color.Red;
                    return;
                }

                //   InsertContactUs(); // to save email in database 
                string senderEmail = txtSenderEmail.Text;
                using (mailMgr myMailMgr = new mailMgr())
                {
                    myMailMgr.mySubject = txtSubject.Text + " " + senderEmail + ": " + senderName;
                    myMailMgr.myBody = txtBody.Text;
                    if (fuAttachment.HasFile)
                    {
                        foreach (HttpPostedFile file in fuAttachment.PostedFiles)
                        {
                            //1. get fileName
                            string fileName = Path.GetFileName(file.FileName);
                            //2. eihter save it to folder or 
                            //. file.SaveAs(Server.MapPath("~/docEmailed/") + fileName);// to save attached files to a folder docEmailed
                            //3. send it to email or 
                            //.  myMailMgr.Attachments.Add(new Attachment(file.InputStream, fileName)); // to attached files to email 

                            // 4. save it to db   
                            //  InsertDocuments(intSenderId);  // works but double entry
                        }
                        rtn = myMailMgr.sendEmailViaGmail(fuAttachment);
                        lblMsg.Text = rtn;
                        lblMsg.ForeColor = Color.Green; // using System.Drawing above 2/2018
                        lblMsg.BackColor = Color.Yellow;
                    }
                    else
                    {
                        rtn = myMailMgr.sendEmailViaGmail();
                    }
                    lblMsg.Text = rtn;
                    lblMsg.ForeColor = Color.Green; // using System.Drawing above 2/2018
                    lblMsg.BackColor = Color.Yellow;
                }
            }

            protected void InsertDocuments(int myClientId)
            {
                // this works 
                foreach (HttpPostedFile postedFile in fuAttachment.PostedFiles)
                {
                    string filename = Path.GetFileName(postedFile.FileName);
                    string contentType = postedFile.ContentType;
                    using (Stream fs = postedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            CRUD DocInsert = new CRUD();
                            string mySql = @"insert into trainerDoc(trainerId,DocName,contentType,DocData) 
                                          values (@trainerId,@DocName,@contentType,@DocData)";
                            Dictionary<string, object> myPara = new Dictionary<string, object>();
                            myPara.Add("@trainerId", myClientId);
                            myPara.Add("@DocName", filename);
                            myPara.Add("@contentType", contentType);
                            myPara.Add("@DocData", bytes);
                            int rtn = DocInsert.InsertUpdateDelete(mySql, myPara);
                            lblMsg.Text = rtn.ToString();
                            // common.PostMsg(lblOutput, "thanks for submitting your data ");
                            //common.PostMsg(lblOutput,rtn,"This is custom msg based on rtn passed");
                        }
                    }
                }


                

            }
            protected void lblOutputClear_txtSubject(object sender, EventArgs e)
            {
                lblMsg.Text = "";
            }

            protected void btnSendViaCode_Click(object sender, EventArgs e)
            {

            }

            
        }
}



