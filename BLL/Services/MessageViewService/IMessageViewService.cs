using System;
using System.Collections.Generic;
using System.Text;


public interface IMessageViewService
{
    string GetMessage();
    string GetStatus();
    void ItemAddedSuccess();
    void ItemAddedError();
    void IncorrectLoginDetails();
    void SuccessfulCheckout();
    void UnsuccessfulCheckout();
    void SuccessfulRegistration();
    void UnsuccesfullRegistration();
}
