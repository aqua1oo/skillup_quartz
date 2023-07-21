namespace Interface.Entity
{
    public class Kfs
    {
        public string ordr_no { get; set; }
        public string ordr_dt { get; set; }
        public string ordr_typ { get; set; }
        public string rls_rsn_cd { get; set; }
        public string sndg_nm { get; set; }
        public string rcip_nm { get; set; }
        public string rcip_tlno { get; set; }
        public string rcip_zpcd { get; set; }
        public string rcip_addr { get; set; }
        public string rcip_addr_dtl { get; set; }        
        public string note { get; set; }
        public string bzpt_prdt_cd { get; set; }
        public int qnt { get; set; }
        public int amt { get; set; }
    }    
}