namespace RestWithASPNETUdemy.Data.VO;

public class TokenVO
{
    
    public bool Authenticatend { get; set; }
    public string Created { get; set; }
    public string Expiration { get; set; }
    public string AcessToken { get; set; }
    public string RefreshToken { get; set; }

    public TokenVO(bool authenticatend, string created, string expiration, string acessToken, string refreshToken)
    {
        Authenticatend = authenticatend;
        Created = created;
        Expiration = expiration;
        AcessToken = acessToken;
        RefreshToken = refreshToken;
    }
}
